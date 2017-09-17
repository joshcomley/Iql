var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
import { Coalesce } from "../Iql/TsUtility/Coalesce";
import { DataContext } from "../Iql/Iql.Queryable/Data/DataContext";
import { Db } from "./Db";
import { Types } from "../Iql/TsUtility/Types";
var AppDbContext = /** @class */ (function (_super) {
    __extends(AppDbContext, _super);
    function AppDbContext(dataStore, evaluateContext) {
        if (dataStore === void 0) { dataStore = null; }
        if (evaluateContext === void 0) { evaluateContext = null; }
        return _super.call(this, Coalesce(dataStore, Types.New_TsConvert_Iql_Queryable_Data_DataStores_InMemory_InMemoryDataStore(Types.New_TsConvert_Iql_JavaScript_QueryToJavaScript_JavaScriptQueryableAdapter())), evaluateContext) || this;
    }
    AppDbContext.prototype.Configure = function (builder) {
        var _this = this;
        Types.New_TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Db();
        builder.DefineEntity(Types.TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person).HasKey(function (p) { return p.Id; }, Number).DefineProperty(function (p) { return p.Name; }, false, String).DefineProperty(function (p) { return p.Age; }, false, Number);
        var config = Types.New_TsConvert_Iql_Queryable_Data_DataStores_InMemory_JavaScriptQueryConfiguration();
        config.RegisterSource(function () { return Db.People; }, Types.TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person);
        this.RegisterConfiguration(config, Types.TsConvert_Iql_Queryable_Data_DataStores_InMemory_JavaScriptQueryConfiguration);
        this.People = Types.New_TsConvert_Iql_Queryable_DbSet(builder, function () { return _this.DataStore; }, null, this, Types.TsConvert_Iql_OData_TypeScript_Generator_ConsoleApp_Library_Person, Number);
    };
    return AppDbContext;
}(DataContext));
export { AppDbContext };
//# sourceMappingURL=C:/Users/josh-xps/AppData/Local/Temp/f21280a4-0f82-4c19-a037-070e78937fa7/Input/Code/AppDbContext.js.map