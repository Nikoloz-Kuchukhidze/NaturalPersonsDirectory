using Mapster;
using NaturalPersonsDirectory.API.Contracts.NaturalPersons;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.AddRelation;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Update;

namespace NaturalPersonsDirectory.API.Common.Mapping;

public sealed class NaturalPersonRequestMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateNaturalPersonRequest, UpdateNaturalPersonCommand>()
            .MapToConstructor(true);

        config.NewConfig<AddRelationRequest, AddRelationCommand>()
            .MapToConstructor(true);
    }
}
