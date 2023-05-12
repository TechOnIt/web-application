using GraphQL.Types;

namespace TechOnIt.Desk.Api.GraphQl.SensorReport;

public class ReportSchema : Schema
{
    public ReportSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<ReportGqQuery>();
        Mutation = serviceProvider.GetRequiredService<ReportGqMutation>();
    }
}


// readme
// graphql in .net 6 : https://dev.to/berviantoleo/getting-started-graphql-in-net-6-part-1-4ic2
