using GraphQL;
using GraphQL.Types;
using TechOnIt.Application.Commands.SensorReports.CreateSensorReport;

namespace TechOnIt.Desk.Api.GraphQl.SensorReport;

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
                var report = new CreateSensorReportCommand
                {
                    Value = value
                };

                reportContext.Send(report);
                return report;
            });
    }
}