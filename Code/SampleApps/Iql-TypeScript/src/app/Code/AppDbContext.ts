import { Coalesce } from "../Iql/TsUtility/Coalesce";
import { EvaluateContext } from "../Iql/Iql.Parsing/EvaluateContext";
import { DbSet } from "../Iql/Iql.Queryable/DbSet";
import { DataContext } from "../Iql/Iql.Queryable/Data/DataContext";
import { DataStore } from "../Iql/Iql.Queryable/Data/DataStores/DataStore";
import { IDataStore } from "../Iql/Iql.Queryable/Data/DataStores/IDataStore";
import { EntityConfigurationBuilder } from "../Iql/Iql.Queryable/Data/EntityConfiguration/EntityConfigurationBuilder";
import { Db } from "./Db";
import { Person } from "./Person";
import { Types } from "../Iql/TsUtility/Types";

export class AppDbContext extends DataContext {
    constructor(dataStore: IDataStore = null, evaluateContext: EvaluateContext = null) {
        super(Coalesce(dataStore, Types.New_TsConvert_Iql_Queryable_Data_DataStores_InMemory_InMemoryDataStore(Types.New_TsConvert_Iql_JavaScript_QueryToJavaScript_JavaScriptQueryableAdapter())), evaluateContext);
    }
    public People: DbSet < Person,
    Number > ;
    public Configure(builder: EntityConfigurationBuilder) : void {
        Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Db();
        builder.DefineEntity<Person> (Types.TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person).HasKey(p =>p.Id, Number).DefineProperty(p =>p.Name, false, String).DefineProperty(p =>p.Age, false, Number);
        let config = Types.New_TsConvert_Iql_Queryable_Data_DataStores_InMemory_JavaScriptQueryConfiguration();
        config.RegisterSource(() =>Db.People, Types.TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person);
        this.RegisterConfiguration(config, Types.TsConvert_Iql_Queryable_Data_DataStores_InMemory_JavaScriptQueryConfiguration);
        this.People = Types.New_TsConvert_Iql_Queryable_DbSet(builder, () =>this.DataStore, null, this, Types.TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person, Number);
    }
}