using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Validators
{
    public sealed class QuestValidator
    {
        public void ValidateAcceptance(Character character, Quest quest)
        {
            if (character.Level < quest.RequiredLevel)
            {
                throw new InvalidOperationException("El personaje no cumple los requisitos para aceptar la misión.");
            }
        }
    }
}

