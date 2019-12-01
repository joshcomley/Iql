using Iql.Data.Tracking.State;

namespace Iql.Data.Extensions
{
    public static class EntityStatusExtensions
    {
        public static bool AreOpposing(this EntityStatus status, EntityStatus newStatus)
        {
            switch (status)
            {
                case EntityStatus.New:
                case EntityStatus.NewAndDeleted:
                    return newStatus != EntityStatus.New && newStatus != EntityStatus.NewAndDeleted;
                case EntityStatus.Existing:
                case EntityStatus.ExistingAndPendingDelete:
                    return newStatus != EntityStatus.Existing && newStatus != EntityStatus.ExistingAndPendingDelete;
            }

            return false;

        }
        public static EntityStatus Opposite(this EntityStatus status)
        {
            if (status == EntityStatus.New)
            {
                return EntityStatus.NewAndDeleted;
            }

            if (status == EntityStatus.NewAndDeleted)
            {
                return EntityStatus.New;
            }

            if (status == EntityStatus.Existing)
            {
                return EntityStatus.ExistingAndPendingDelete;
            }

            if (status == EntityStatus.ExistingAndPendingDelete)
            {
                return EntityStatus.Existing;
            }

            return status;
        }

        public static bool WillUntrack(this EntityStatus status)
        {
            return status == EntityStatus.Existing ||
                   status == EntityStatus.ExistingAndPendingDelete ||
                   status == EntityStatus.New;
        }
    }
}