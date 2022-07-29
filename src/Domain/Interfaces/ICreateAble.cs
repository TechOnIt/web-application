using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Domain.Interfaces;

public interface ICreateAble
{
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
}