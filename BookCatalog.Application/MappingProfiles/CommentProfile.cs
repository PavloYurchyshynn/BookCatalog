using AutoMapper;
using BookCatalog.Application.Models.Comment;
using BookCatalog.Core.Entities;

namespace BookCatalog.Application.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile() 
        {
            CreateMap<Comment, CommentModel>();
            CreateMap<CommentModel, Comment>();
            CreateMap<Comment, AddCommentModel>();
            CreateMap<AddCommentModel, Comment>();
        }
    }
}
