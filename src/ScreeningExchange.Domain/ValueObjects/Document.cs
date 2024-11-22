using System.Text.RegularExpressions;
using ScreeningExchange.Domain.Enums;

namespace ScreeningExchange.Domain.Aggregates.ValueObjects;

public sealed class Document
{
    private static readonly int[] Multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] Multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    public string DocumentNumber { get; private set; }
    public TypeDocumentEnum TypeDocument { get; private set; }
    public bool IsValid { get; private set; }

    private Document(string documentNumber, TypeDocumentEnum typeDocument)
    {
        if (string.IsNullOrWhiteSpace(documentNumber))
            throw new ArgumentNullException(nameof(documentNumber));

        var document = string.Join("", new Regex(@"\d+").Matches(documentNumber));

        if (typeDocument == TypeDocumentEnum.Cpf && document.Length != 11)
            throw new ArgumentException("The document is invalid");

        if (typeDocument == TypeDocumentEnum.Cnpj && document.Length != 16)
            throw new ArgumentException("The document is invalid");

        var isValidDocument = IsValidDocument(documentNumber, typeDocument);

        if (isValidDocument is false)
            throw new ArgumentException($"The number document is invalid. type: {typeDocument} value: {documentNumber}");

        TypeDocument = typeDocument;
        DocumentNumber = document;
        IsValid = true;
    }

    public static Document Create(string value, TypeDocumentEnum typeDocument)
        => new(value, typeDocument);

    private static bool IsValidCpf(string value)
    {
        if (value == null) return false;

        var posicao = 0;
        var totalDigito1 = 0;
        var totalDigito2 = 0;
        var dv1 = 0;
        var dv2 = 0;

        bool digitosIdenticos = true;
        var ultimoDigito = -1;

        foreach (var c in value)
        {
            if (char.IsDigit(c))
            {
                var digito = c - '0';
                if (posicao != 0 && ultimoDigito != digito)
                {
                    digitosIdenticos = false;
                }

                ultimoDigito = digito;
                if (posicao < 9)
                {
                    totalDigito1 += digito * (10 - posicao);
                    totalDigito2 += digito * (11 - posicao);
                }
                else if (posicao == 9)
                {
                    dv1 = digito;
                }
                else if (posicao == 10)
                {
                    dv2 = digito;
                }

                posicao++;
            }
        }

        if (posicao > 11)
        {
            return false;
        }

        if (digitosIdenticos)
        {
            return false;
        }

        var digito1 = totalDigito1 % 11;
        digito1 = digito1 < 2
            ? 0
            : 11 - digito1;

        if (dv1 != digito1)
        {
            return false;
        }

        totalDigito2 += digito1 * 2;
        var digito2 = totalDigito2 % 11;
        digito2 = digito2 < 2
            ? 0
            : 11 - digito2;

        return dv2 == digito2;
    }

    private static bool IsValidCnpj(string value)
    {
        if (value == null)
            return false;

        var digitosIdenticos = true;
        var ultimoDigito = -1;
        var posicao = 0;
        var totalDigito1 = 0;
        var totalDigito2 = 0;

        foreach (var c in value)
        {
            if (char.IsDigit(c))
            {
                var digito = c - '0';
                if (posicao != 0 && ultimoDigito != digito)
                {
                    digitosIdenticos = false;
                }

                ultimoDigito = digito;
                if (posicao < 12)
                {
                    totalDigito1 += digito * Multiplicador1[posicao];
                    totalDigito2 += digito * Multiplicador2[posicao];
                }
                else if (posicao == 12)
                {
                    var dv1 = (totalDigito1 % 11);
                    dv1 = dv1 < 2
                        ? 0
                        : 11 - dv1;

                    if (digito != dv1)
                        return false;

                    totalDigito2 += dv1 * Multiplicador2[12];
                }
                else if (posicao == 13)
                {
                    var dv2 = (totalDigito2 % 11);

                    dv2 = dv2 < 2
                        ? 0
                        : 11 - dv2;

                    if (digito != dv2)
                        return false;
                }

                posicao++;
            }
        }

        return (posicao == 14) && !digitosIdenticos;
    }
    private static bool IsValidDocument(string value, TypeDocumentEnum typeDocument)
    {
        if (typeDocument == TypeDocumentEnum.Cnpj)
            return IsValidCnpj(value);

        return IsValidCpf(value);
    }
}