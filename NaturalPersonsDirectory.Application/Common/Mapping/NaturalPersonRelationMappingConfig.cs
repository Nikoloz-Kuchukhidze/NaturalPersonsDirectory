using Mapster;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Common.Mapping;

public sealed class NaturalPersonRelationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<NaturalPersonRelationCommand, NaturalPersonRelation>()
            .MapToConstructor(true);
    }
}
