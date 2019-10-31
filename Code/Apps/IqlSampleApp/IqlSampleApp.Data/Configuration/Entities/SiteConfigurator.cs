using System.Linq;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql;
using Iql.Entities;
using Iql.Forms;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.OData.NetTopology;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class SiteIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            var sites = builder.EntityType<Site>();
            sites.HasNestedSet(s => s.Left, s => s.Right, s => s.LeftOf, s => s.RightOf,
                s => s.Key, s => s.Level, s => s.ParentId, s => s.Parent, s => s.Id);
            // sites.ConfigureProperty(_ => _.ClientId, _ => _.IsConditionallyInferredWith(p => p.CurrentEntityState.ClientId, p => IqlCurrentUser.Get<ApplicationUser>().ClientId != null));
            sites.DefinePropertyDisplayRule(sites.NestedSets[0], _ => _.ClientId != 0);
            sites
                .ConfigureProperty(_ => _.InferredChainFromSelf,
                    _ =>
                    {
                        _.IsInferredWith(site => site.CurrentEntityState.InferredChainFromUserName);
                    })
                .ConfigureProperty(_ => _.InferredChainFromUserName,
                    _ =>
                    {
                        _.IsInferredWith(site => IqlCurrentUser.Get<ApplicationUser>().UserName);
                    })
                .ConfigureProperty(_ => _.Key,
                    _ =>
                    {
                        _.IsInferredWith(site => site.CurrentEntityState.ClientId);
                    })
                .ConfigureProperty(_ => _.FullAddress,
                    _ =>
                    {
                        _.IsInferredWith(site => site.CurrentEntityState.Address + "\n" + site.CurrentEntityState.PostCode);
                    })                
                .DefinePropertyValidation(_ => _.FullAddress, _ => _.FullAddress == null || _.FullAddress == "")
                .SetEditDisplay((entityConfiguration, displayConfiguration) =>
                {
                    displayConfiguration.SetProperties(
                        entityConfiguration,
                        _ => _.FindRelationship(s => s.Client).Configure(c => c.EditKind = PropertyEditKind.Edit),
                        _ => _.FindPropertyByExpression(s => s.Name),
                        _ => _.FindRelationship(s => s.Parent),
                        _ => _.PropertyCollection(
                            pc => pc.FindPropertyByExpression(s => s.Address),
                            pc => pc.FindPropertyByExpression(s => s.PostCode)
                        ).Configure(c =>
                        {
                            c.ContentAlignment = ContentAlignment.Horizontal;
                            c.Name = "Site Address";
                            c.SetHint(FormHints.HelpTextTop);
                        }),
                        _ => _.FindPropertyByExpression(s => s.Parent),
                        _ => _.FindPropertyByExpression(s => s.Key),
                        _ => _.FindPropertyByExpression(s => s.Location)
                    );
                });
            var areas = sites.FindCollectionRelationship(_ => _.Areas);
            //areas.CanWrite = true;
            areas.EditKind = PropertyEditKind.Edit;
        }
    }

    public class SiteConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder.MapSpatial<Site>(_ => _.EdmLocation, _ => _.Location);
            builder.MapSpatial<Site>(_ => _.EdmArea, _ => _.Area);
            builder.MapSpatial<Site>(_ => _.EdmLine, _ => _.Line);
            builder.EntityType<Site>()
                .HasOptional(s => s.Parent, (left, right) => left.ParentId == right.Id, site => site.Children);
            builder.EntityType<Site>()
                .HasOptional(s => s.Client, (left, right) => left.ClientId == right.Id, client => client.Sites);
        }
    }
}