using System;

namespace ScreeningExchange.Domain.Aggregates.UsuarioAggregate;

public class Usuario
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Celular { get; private set; }
    public DateTime CreatedOn { get; private set; }
}