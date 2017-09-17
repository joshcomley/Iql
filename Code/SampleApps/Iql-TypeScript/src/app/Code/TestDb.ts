import { Types } from "../Iql/TsUtility/Types";

export class TestDb {
    public static async Run() : Promise<any> {
        let db = Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_AppDbContext();
        let cara = await db.People.OrderBy(p =>p.Name, null, String).ToListWithResponse();
        cara.Data.forEach((person) =>{
            console.log(person.Name + ` - ` + person.Age);
        });
    }
}