using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Acme.BookStore.BackgroundJob;
using Acme.BookStore.Common;
using Acme.BookStore.Permissions;
using Acme.BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Authors;

[Authorize(Roles = "admin ,Default")]
[Authorize(BookStorePermissions.Authors.Default)]
public class AuthorAppService : BookStoreAppService, IAuthorAppService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly AuthorManager _authorManager;
    private readonly IBackgroundJobManager _backgroundJobManager;
    private readonly IImageService _imageService;
    private readonly ImageSettings _imageSettings;
    private readonly IDataFilter<ISoftDelete> _dataFilter;
    public AuthorAppService(IDataFilter<ISoftDelete> dataFilter, IOptions<ImageSettings> imageSettings, IImageService imageService, IBackgroundJobManager backgroundJobManager, IAuthorRepository authorRepository, AuthorManager authorManager)
    {
        _authorRepository = authorRepository;
        _authorManager = authorManager;
        _backgroundJobManager = backgroundJobManager;
        _imageService = imageService;
        _imageSettings = imageSettings.Value;
        _dataFilter = dataFilter;
    }

    public async Task<AuthorDto> GetAsync(Guid id)
    {
        var author = await _authorRepository.GetAsync(id);
        await _backgroundJobManager.EnqueueAsync(new EmailSendingArgs { EmailAddress = "mmoussa@sure.com.sa", Subject = "You've successfully get your data!", Body = "..." }, priority: BackgroundJobPriority.High, delay: TimeSpan.FromSeconds(1));
        return ObjectMapper.Map<Author, AuthorDto>(author);
    }
    public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
    {
        //using (_dataFilter.Disable())
        //{
        if (input.Sorting.IsNullOrWhiteSpace())
            input.Sorting = nameof(Author.Name);


        var authors = await _authorRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

        var totalCount = input.Filter == null
            ? await _authorRepository.CountAsync()
            : await _authorRepository.CountAsync(author => author.Name.Contains(input.Filter));

        return new PagedResultDto<AuthorDto>(totalCount, ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors));
        //}
    }

    [Authorize(BookStorePermissions.Authors.Create)]
    public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
    {
        var (isImageUploaded, filepath) = await _imageService.UploadImage(input.Image, _imageSettings.RootImagePath, _imageSettings.AuthorImagesFolder);
        var author = await _authorManager.CreateAsync(input.Name, input.BirthDate, input.ShortBio, isImageUploaded ? filepath : null);
        await _authorRepository.InsertAsync(author);

        return ObjectMapper.Map<Author, AuthorDto>(author);
    }

    [Authorize(BookStorePermissions.Authors.Edit)]
    public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
    {
        var author = await _authorRepository.GetAsync(id);

        if (author.Name != input.Name)
            await _authorManager.ChangeNameAsync(author, input.Name);
        
        if (input.Image != null && input.Image.Length > 0 )
        {
            var (isImageUploaded, filepath) = await _imageService.UploadImage(input.Image, _imageSettings.RootImagePath, _imageSettings.AuthorImagesFolder);
            if (isImageUploaded)
                author.ImagePath = filepath;
        }
        
        author.BirthDate = input.BirthDate;
        author.ShortBio = input.ShortBio;
        await _authorRepository.UpdateAsync(author);
    }
    [Authorize(BookStorePermissions.Authors.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _authorRepository.DeleteAsync(id);
    }

}
