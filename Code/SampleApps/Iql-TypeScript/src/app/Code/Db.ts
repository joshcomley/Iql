import { Person } from "./Person";
import { Types } from "../Iql/TsUtility/Types";

export class Db {
    constructor() {
        if (!Db.Initialized) {
            Db.Initialized = true;
            Db.People = new Array<Person> ();
            Db.People.push((() =>{
                let obj = Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person();
                obj.Age = 32;
                obj.Id = 2;
                obj.Name = `Cara`;;
                return obj;
            })());
            Db.People.push((() =>{
                let obj = Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person();
                obj.Age = 24;
                obj.Id = 1;
                obj.Name = `Paulina`;;
                return obj;
            })());
            Db.People.push((() =>{
                let obj = Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person();
                obj.Age = 31;
                obj.Id = 3;
                obj.Name = `Kiera`;;
                return obj;
            })());
        }
    }
    public static People: Array<Person> ;
    private static Initialized: boolean = false;
}