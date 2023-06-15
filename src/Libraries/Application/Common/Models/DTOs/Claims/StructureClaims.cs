namespace TechOnIt.Application.Common.Models.DTOs.Claims;

public class StructureClaims
{
    #region constructor
    public StructureClaims(Guid StructureId,string StructureName, string ApiKey)
    {
        this.StructureId = StructureId;
        this.StructureName = StructureName;
        this.ApiKey = ApiKey;
    }
    #endregion

    public string StructureName { get; set; }
    public Guid StructureId { get; set; }
    public string ApiKey { get; set; }
}
