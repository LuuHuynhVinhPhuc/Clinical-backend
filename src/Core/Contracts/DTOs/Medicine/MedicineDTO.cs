namespace ClinicalBackend.Contracts.DTOs.Medicine
{
    public record struct MedicineDto(
        Guid Id,
        string? Name,
        string? Company,
        int Stock,
        float Price,
        string? Type,
        string? Status,
        DateTime CreatedAt
    );

    public record struct MedicineByDateDto(
        Guid Id,
        string? Name,
        string? Company,
        int Amount,
        float CombinedPrice,
        string? Type,
        DateTime CreatedAt
    );

    public record struct Top10MedicineDto(
        Guid Id,
        string? Name,
        string? Company,
        int Amount
    );
}

