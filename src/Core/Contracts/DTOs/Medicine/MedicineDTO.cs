namespace ClinicalBackend.Contracts.DTOs.Medicine
{
    public record struct MedicineDto(
        Guid Id,
        string? Name,
        string? Company,
        string? Specialty,
        string? Nutritional,
        string? Dosage,
        int Stock,
        float Price,
        string? Status,
        DateTime CreatedAt
    );

    public record struct MedicineByDateDto(
        Guid Id,
        string? Name,
        string? Company,
        int Amount,
        float CombinedPrice,
        DateTime CreatedAt
    );

    public record struct Top10MedicineDto(
        Guid Id,
        string? Name,
        string? Company,
        int Amount
    );
}

