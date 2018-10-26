namespace IqlSampleApp.Data.Entities
{
    public enum InspectionFailReason
    {
        None,
        UnableToAccess, // ExclusionZone
        PersistentFaults,
        FailuresInFaultReports,
        TooManyMinorObservations,
        NoDesignSupplied
    }
}
