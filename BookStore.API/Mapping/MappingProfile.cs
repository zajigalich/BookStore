using AutoMapper;
using BookStore.API.Models;
using BookStore.API.DTOs;

namespace BookStore.API.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Map Book -> BookDto
			CreateMap<Book, BookDto>()
				.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
				.ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

			// Map Author -> AuthorDto
			CreateMap<Author, AuthorDto>();

			// Map Genre -> GenreDto
			CreateMap<Genre, GenreDto>();

			// Map CreateBookDto -> Book
			CreateMap<CreateBookDto, Book>();

			// Map UpdateBookDto -> Book
			CreateMap<UpdateBookDto, Book>();

			CreateMap<CreateAuthorDto, Author>();

			CreateMap<UpdateAuthorDto, Author>();

			CreateMap<CreateGenreDto, Genre>();

			CreateMap<UpdateGenreDto, Genre>();
		}
	}
}