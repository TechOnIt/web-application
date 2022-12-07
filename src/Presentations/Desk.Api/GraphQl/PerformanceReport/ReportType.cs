using GraphQL.Types;

namespace TechOnIt.Desk.Api.GraphQl.PerformanceReport;

public class ReportType : ObjectGraphType<ReportGqModel>
{
    public ReportType()
    {
        Name = "Reports";
        Description = "Device and Sensors Reports";

        Field(d => d.Id, nullable: false).Description("report Id");
        Field(d => d.Value, nullable: false).Description("the main parameter of report");
        Field(d => d.RecordDateTime, nullable: false).Description("record report date time");
    }
}
