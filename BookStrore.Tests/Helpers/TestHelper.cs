using AutoMapper;
using BookStore.API.Mapping;

namespace BookStrore.Tests.Helpers
{
	public static class TestHelper
	{
		public static IMapper CreateMap()
		{
			var profile = new MappingProfile();
			var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
			return new Mapper(configuration);
		}
	}
}
