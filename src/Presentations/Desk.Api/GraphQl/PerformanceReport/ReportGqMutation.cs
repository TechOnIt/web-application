using GraphQL;
using GraphQL.Types;
using TechOnIt.Application.Commands.PerformanceReports.CreatePerformanceReport;
using MediatR;

namespace TechOnIt.Desk.Api.GraphQl.PerformanceReport;

public class ReportGqMutation : ObjectGraphType
{
    public ReportGqMutation()
    {
        Field<ReportType>(
            "CreateReport",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "Value" }),
            resolve: context =>
            {
                var value = context.GetArgument<int>("Value");
                var reportContext = context.RequestServices.GetRequiredService<IMediator>();
                var report = new CreatePerformanceReportCommand
                {
                    Value = value
                };

                reportContext.Send(report);
                return report;
            });
    }
}