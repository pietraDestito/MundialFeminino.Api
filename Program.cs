
using Microsoft.EntityFrameworkCore;
using MundialFeminino.Api.Data;
using MundialFeminino.Api.Data.ImportacaoCsv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexaoMundial")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

string caminhoArquivo = Path.Combine(
    app.Environment.ContentRootPath,
    "Data",
    "Importacao",
    "fonte_rsssf.html");

// Comando de verificação: não grava dados no banco.
if (args.Contains("--verificar-artilheiras"))
{
    var registros = LeitorArtilheiras.LerArquivo(caminhoArquivo);

    ImportadorArtilheiras.Validar(registros);

    Console.WriteLine("\nRegistros por edição:");

    foreach (var grupo in registros
        .GroupBy(j => j.Ano)
        .OrderBy(g => g.Key))
    {
        Console.WriteLine(
            $"{grupo.Key}: {grupo.Count()} registros, " +
            $"{grupo.Sum(j => j.Gols)} gols");
    }

    return;
}

// Comando explícito para importar o histórico.
if (args.Contains("--importar-artilheiras"))
{
    var registros = LeitorArtilheiras.LerArquivo(caminhoArquivo);

    using var escopo = app.Services.CreateScope();

    var contexto = escopo.ServiceProvider
        .GetRequiredService<AppDbContext>();

    await ImportadorArtilheiras.ImportarHistoricoAsync(
        contexto,
        registros);

    return;
}

// Comando de verificação do CSV: não grava dados no banco.
if (args.Contains("--verificar-csv-2027"))
{
    string caminhoCsv = Path.Combine(
        app.Environment.ContentRootPath,
        "Importacoes",
        "artilheiras-2027.csv");

    var leitor = new LeitorArtilheirasCsv();
    var registros = leitor.Ler(caminhoCsv);

    Console.WriteLine(
        $"\nTotal de registros lidos: {registros.Count}");

    foreach (var registro in registros)
    {
        Console.WriteLine(
            $"{registro.Nome} | {registro.Selecao} | " +
            $"{registro.Ano} | {registro.Gols} gols");
    }

    Console.WriteLine("\nCSV lido e validado com sucesso!");

    return;
}


// Comando explícito para importar o CSV de 2027.
if (args.Contains("--importar-csv-2027"))
{
    string caminhoCsv = Path.Combine(
        app.Environment.ContentRootPath,
        "Importacoes",
        "artilheiras-2027.csv");

    // Primeiro, lê e valida todo o arquivo.
    var leitor = new LeitorArtilheirasCsv();
    var registros = leitor.Ler(caminhoCsv);

    // Obtém acesso ao banco de dados.
    using var escopo = app.Services.CreateScope();

    var contexto = escopo.ServiceProvider
        .GetRequiredService<AppDbContext>();

    // Importa ou atualiza os registros de 2027.
    var importador = new ImportadorArtilheirasCsv();

    int quantidade = await importador.ImportarAsync(
        contexto,
        registros);

    Console.WriteLine(
        $"\nImportação concluída! Registros processados: {quantidade}");

    return;
}


app.UseSwagger(options =>
{
    options.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "Mundial Feminino API v1");

    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

