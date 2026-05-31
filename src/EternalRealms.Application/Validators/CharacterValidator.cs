namespace EternalRealms.Application.Validators
{
    public sealed class CharacterValidator
    {
        public void Validate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("El nombre del personaje no puede estar vacío.", nameof(name));
            }

            if (name.Length > 24)
            {
                throw new ArgumentException("El nombre del personaje no puede superar los 24 caracteres.", nameof(name));
            }
        }
    }
}

