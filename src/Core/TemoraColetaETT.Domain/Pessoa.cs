namespace TemoraColetaETT.Domain;

public class Pessoa
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    public required string Nome { get; init; }
    public required DateTime DataNascimento { get; init; }
    public required string Rg { get; init; }
    public required string Cpf { get; init; }
    public required string OrgaoEmissor { get; init; }
    public required string UfEmissor { get; init; }
    public required Guid EmpresaId { get; init; }

    public virtual void Validate()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("Nome é obrigatório.");

        if (DataNascimento > DateTime.Today)
            throw new ArgumentException("Data de nascimento inválida.");

        if (Cpf.Length != 11)
            throw new ArgumentException("CPF inválido.");

        if (string.IsNullOrWhiteSpace(Rg))
            throw new ArgumentException("RG é obrigatório.");

        if (EmpresaId == Guid.Empty)
            throw new ArgumentException("Pessoa deve estar associada a uma empresa.");
    }

    public static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11) return false;
        if (cpf.Distinct().Count() == 1) return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }
}