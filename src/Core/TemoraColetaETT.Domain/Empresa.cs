using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace TemoraColetaETT.Domain;

public class Empresa
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    public string? RazaoSocial { get; private set; }
    public string? Cnpj { get; private set; }
    public required string Localizacao { get; init; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Cnpj) || !ValidarCnpj(Cnpj))
            throw new ArgumentException("CNPJ inválido.");

        if (string.IsNullOrWhiteSpace(Localizacao))
            throw new ArgumentException("Localização é obrigatória.");
    }

    public static bool ValidarCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) return false;

        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14)return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string temp = cnpj.Substring(0, 12);
        for (int j = 0; j < 2; j++)
        {
            int[] mult = j == 0 ? multiplicador1 : multiplicador2;
            int soma = temp.Select((c, i) => (c - '0') * mult[i]).Sum();
            int dig = soma % 11 < 2 ? 0 : 11 - soma % 11;
            temp += dig;
        }

        return cnpj.EndsWith(temp[^2..]);
    }

    public async Task ValidateWithApiAsync(string cnpj)
    {
        using var client = new HttpClient();
        string url = $"https://publica.cnpj.ws/cnpj/{cnpj}";

        // HttpResponseMessage response = await client.GetAsync(url);

        // if (!response.IsSuccessStatusCode)
            // throw new Exception($"Erro ao validar CNPJ via API. StatusCode: {response.StatusCode}");

        // using JsonDocument doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        // JsonElement root = doc.RootElement;

        // RazaoSocial = root.GetProperty("razao_social").GetString() ?? "";
        RazaoSocial = "Empresa 1";
        // Cnpj = root.GetProperty("estabelecimento").GetProperty("cnpj").GetString() ?? "";
        Cnpj = cnpj;
        Validate();
    }
}