using Mapster;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Common.Mapping;

public sealed class NaturalPersonMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<NaturalPerson, NaturalPersonResponse>()
            .MapToConstructor(true)
            .Map(dest => dest.City, src => src.City!.Name);
    }
}
