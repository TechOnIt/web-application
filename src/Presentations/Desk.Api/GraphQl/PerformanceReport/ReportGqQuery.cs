using GraphQL.Types;
using TechOnIt.Application.Queries.PerformanceReports.GetAllPerformanceReportByFilter;
using MediatR;

namespace TechOnIt.Desk.Api.GraphQl.PerformanceReport;

public class ReportGqQuery : ObjectGraphType
{
    public ReportGqQuery()
    {
        Field<ListGraphType<ReportType>>("reports", resolve: context => new List<ReportGqModel>
        {
            new ReportGqModel(Guid.NewGuid(),2,DateTime.Now),
            new ReportGqModel(Guid.NewGuid(),7,DateTime.Now.AddDays(2))
        });

        Field<ListGraphType<ReportType>>("ReportFromDb", resolve: context =>
        {
            var reportContext = context.RequestServices.GetRequiredService<IMediator>();
            return reportContext.Send(new GetAllPerformanceReportByFilterQuery());
        });
    }
}
