import { EvaluateContext } from "../Iql/Iql.Parsing/EvaluateContext";
import { DbSet } from "../Iql/Iql.Queryable/DbSet";
import { DataContext } from "../Iql/Iql.Queryable/Data/DataContext";
import { IDataStore } from "../Iql/Iql.Queryable/Data/DataStores/IDataStore";
import { EntityConfigurationBuilder } from "../Iql/Iql.Queryable/Data/EntityConfiguration/EntityConfigurationBuilder";
import { Person } from "./Person";
export declare class AppDbContext extends DataContext {
    constructor(dataStore?: IDataStore, evaluateContext?: EvaluateContext);
    People: DbSet<Person, Number>;
    Configure(builder: EntityConfigurationBuilder): void;
}
