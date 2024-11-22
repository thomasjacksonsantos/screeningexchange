using System.Security.Cryptography;
using System.Text;

namespace ScreeningExchange.App.Api.Features.Shared;

public static class UidExtensions
{
    public static Ulid ConvertUidToUlid(this string uid)
    {
        // Converter user_id no formato uid do firebase para guid e depois para Ulid
        using MD5 md5 = MD5.Create();
        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(uid));
        var uuid = new Guid(hash);

        return new Ulid(uuid);
    }

    public static Ulid? ConvertToULidOrNull(this string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;

        return Ulid.Parse(id);
    }

    public static Ulid ConvertToULid(this string id)
        => Ulid.Parse(id);
}