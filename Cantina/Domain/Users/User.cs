using Flunt.Notifications;
using Flunt.Validations;

namespace CantinaWebAPI.Domain.Users
{
    public class User : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identification { get; set; }//CPF
        public DateOnly DateOfBirth { get; set; }
        public string Password { get; set; }

        public static bool IsCPFValid(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            string digitoPos = cpf.Substring(0, 1);
            bool digitosIguais = true;
            for(int i = 1; i < cpf.Length; i++)
            {
                if (!digitoPos.Equals(cpf.Substring(i, 1)))
                {
                    digitosIguais = false;
                    break;
                }
            }

            if (digitosIguais)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public User(string name, string email, string password, string identification, DateOnly dateOfBirth) 
        {
            var contract = new Contract<User>()
                .IsNotNullOrEmpty(name, "Name", "Nome é obrigatório!")
                .IsGreaterOrEqualsThan(password, 4, "Password", "Senha deve conter no mínimo 4 dígitos")
                .IsNotNullOrEmpty(email, "Email", "E-mail é obrigatório!")
                .IsEmail(email, "Email", "E-mail inválido!")
                .IsNotNull(password, "Password", "Senha deve ser informada!")
                .IsNotNullOrEmpty(identification, "Identification", "CPF deve ser informado!")
                .IsTrue(IsCPFValid(identification), "Identification", "Informe um CPF Válido!")
                .IsNotNull(dateOfBirth, "DateOfBirth", "Data de nascimento deve ser informada!")
                .IsBetween(DateTime.Parse(dateOfBirth.ToString()), DateTime.Now.AddYears(-100), DateTime.Today, "DateOfBirth", "Data de Nascimento deve estar em um intervalo válido");
                
            AddNotifications(contract);

            Name = name;
            Email = email;
            Password = password;
            Identification = identification;
            DateOfBirth = dateOfBirth;
        }
    }
}
