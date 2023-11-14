using AdvertisementsBoard.Contracts.Comments;
using AdvertisementsBoard.Domain.Comments;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Comments;

/// <summary>
///     Профиль маппера для подкатегорий.
/// </summary>
public class CommentProfile : Profile
{
    /// <summary>
    ///     Конструктор, настраивающий маппинг между моделями для комментариев.
    /// </summary>
    public CommentProfile()
    {
        CreateMap<Comment, CommentInfoDto>();
        CreateMap<Comment, CommentUpdatedDto>();

        CreateMap<CommentCreateDto, Comment>().IgnoreAllNonExisting();
        CreateMap<CommentUpdatedDto, Comment>().IgnoreAllNonExisting();
    }
}