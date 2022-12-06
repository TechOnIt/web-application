using iot.Domain.Entities.Product.StructureAggregate;
using iot.Domain.Enums;

namespace iot.Application.Common.ViewModels.Structures;

public class StructureViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public StuctureType Type { get; set; }
    public Concurrency ApiKey { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }

    #region explicit casting

    public static explicit operator Structure(StructureViewModel viewModel)
    {
        return new Structure()
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Description = viewModel.Description,
            IsActive = viewModel.IsActive
        };
    }
    #endregion
}