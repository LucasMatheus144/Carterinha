using System.ComponentModel.DataAnnotations;

namespace Carterinha.Aplication.Services
{
    public class ValidatorService
    {

        public bool ValidarDto<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "O objeto não pode ser nulo.");
            }

            return true;
        }

        public bool ValidaEntidade<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "A entidade não pode ser nula.");
            }

            var valida = Validator.TryValidateObject(obj, new ValidationContext(obj), null, true);
            if (!valida)
            {
                var erros = new List<ValidationResult>();
                throw new ValidationException($"{typeof(T).Name} ->> {string.Join(", ", erros.Select(e => e.ErrorMessage))}");
            }

            return valida;
        }
    }

    public class ValidatorCpfAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var cpf = value as string;

            if (string.IsNullOrWhiteSpace(cpf))
                return new ValidationResult("Cpf field is required", new[] { validationContext.MemberName! });

            //Aqui valida o CPF é valido ou não
            var isValid = CpfValidar.ValidarCpf(cpf);

            return isValid ? ValidationResult.Success : new ValidationResult("CPF inválido", new[] { validationContext.MemberName! });

        }
    }

    // Documentação https://www.macoratti.net/11/09/c_val1.htm
    public static class CpfValidar
    {
        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11 || cpf.All(c => c == cpf[0])) return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf[..9];
            var soma = tempCpf
                .Select((t, i) => int.Parse(t.ToString()) * multiplicador1[i])
                .Sum();

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = tempCpf
                .Select((t, i) => int.Parse(t.ToString()) * multiplicador2[i])
                .Sum();

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith($"{digito1}{digito2}");
        }
    }
}
