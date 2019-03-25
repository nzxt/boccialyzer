using Boccialyzer.Core.Context;
using Boccialyzer.Domain.Entities;
using Serilog;
using System;
using System.Threading.Tasks;
using Boccialyzer.Domain.Enums;

namespace Boccialyzer.Core.SeedData
{
    public static class BallData
    {
        #region # Array Balls

        private static readonly Ball[] Balls =
        {
            new Ball
            {
                Id = new Guid("a719bc37-5607-40d5-b0e6-299ecae32d0d"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=1,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.Violation,
                ShotType=ShotType.BounceOver,
                Box=Box.Box3,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("df344bfa-f6aa-4ee6-9e87-f436f8711506")
            },
            new Ball
            {
                Id = new Guid("a80dfe5b-83a8-468b-a42a-d2e611ace63a"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=2,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.LobbingShot,
                Box=Box.Box3,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("df344bfa-f6aa-4ee6-9e87-f436f8711506")
            },
            new Ball
            {
                Id = new Guid("7df2e8e2-6701-4dc4-a7f9-347b90a23aa3"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=3,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.LobbingShot,
                Box=Box.Box3,
                Distance=Distance.From25To30,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("df344bfa-f6aa-4ee6-9e87-f436f8711506")
            },
            new Ball
            {
                Id = new Guid("8a344d77-0644-4cf0-afd4-cb822a3cb1ca"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.Violation,
                ShotType=ShotType.Ricochet,
                Box=Box.Box4,
                Distance=Distance.From35To40,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("df344bfa-f6aa-4ee6-9e87-f436f8711506")
            },
            new Ball
            {
                Id = new Guid("8e3219a3-95ca-4ce3-bc36-1ca0d0f7990e"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.RollOnTop,
                Box=Box.Box4,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("df344bfa-f6aa-4ee6-9e87-f436f8711506")
            },
            new Ball
            {
                Id = new Guid("eca5e9a1-0d9c-4707-9a4e-b9399931e7f0"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.RollUpAndOver,
                Box=Box.Box4,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("dd1c8c0f-b5f3-47ad-9e0e-267f3b58fa37")
            },
            new Ball
            {
                Id = new Guid("8fab43f8-9171-4666-a13a-55951a18ee7b"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.Smash,
                Box=Box.Box4,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("dd1c8c0f-b5f3-47ad-9e0e-267f3b58fa37")
            },
            new Ball
            {
                Id = new Guid("c45ea264-78fe-44d0-8a05-fae84f75a3b1"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box1,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("dd1c8c0f-b5f3-47ad-9e0e-267f3b58fa37")
            },
            new Ball
            {
                Id = new Guid("73138085-3758-4c7c-ba10-08e5ff0bae7e"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.Placement,
                Box=Box.Box1,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("dd1c8c0f-b5f3-47ad-9e0e-267f3b58fa37")
            },
            new Ball
            {
                Id = new Guid("8c5bf2df-16cd-4224-bddb-051514d1e839"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box1,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("a37dda83-944e-4b13-8167-cab2a969bb6f")
            },
            new Ball
            {
                Id = new Guid("c163a239-c31c-4efa-85ea-4e68a8870296"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From20To25,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("a37dda83-944e-4b13-8167-cab2a969bb6f")
            },
            new Ball
            {
                Id = new Guid("5b9b86cb-4ab8-4273-9889-512ed852ccb9"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("a37dda83-944e-4b13-8167-cab2a969bb6f")
            },
            new Ball
            {
                Id = new Guid("d6220fab-f982-4c26-a6a1-a820cb640226"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("a37dda83-944e-4b13-8167-cab2a969bb6f")
            },
            new Ball
            {
                Id = new Guid("3561bbdc-ef3f-43a2-b1f4-3a574c630eb6"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("a37dda83-944e-4b13-8167-cab2a969bb6f")
            },
            new Ball
            {
                Id = new Guid("cf47084c-2ef7-4012-80e0-c6a6c24e039b"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("e60ad5be-fe37-48e3-997c-f55e68e18202")
            },
            new Ball
            {
                Id = new Guid("73a39c9b-8e0f-4e2a-a701-78429708add2"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("e60ad5be-fe37-48e3-997c-f55e68e18202")
            },
            new Ball
            {
                Id = new Guid("760b53a5-b792-4ec9-95ea-b23a8ce0cec5"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("e60ad5be-fe37-48e3-997c-f55e68e18202")
            },
            new Ball
            {
                Id = new Guid("c5e678b0-739e-4698-a4ae-3db50ba74bf9"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("9bf64876-930b-4dcd-a5ef-4621d9d08240")
            },
            new Ball
            {
                Id = new Guid("8a4f1ecd-3481-4e5d-98b7-d7769ca9631a"),
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
                Rating=4,
                IsPenalty=false,
                IsDeadBall=false,
                DeadBallType=DeadBallType.None,
                ShotType=ShotType.BounceOver,
                Box=Box.Box6,
                Distance=Distance.From15To20,
                PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
                TrainingId = new Guid("9bf64876-930b-4dcd-a5ef-4621d9d08240")
            },



            //new Ball
            //{
            //Id = new Guid(""),
            //CreatedOn = DateTime.UtcNow.AddDays(-1),
            //CreatedBy = new Guid("a5922060-c876-4d1e-ab4e-1fe8dfcd987b"),
            //Rating=4,
            //IsPenalty=false,
            //IsDeadBall=false,
            //DeadBallType=DeadBallType.None,
            //ShotType=ShotType.BounceOver,
            //Box=Box.Box6,
            //Distance=Distance.From15To20,
            //StageId = new Guid(""),
            //PlayerId = new Guid("58d7216a-db42-4bea-86f7-aa1c16f5187e"),
            //TrainingId = new Guid("")
            //}
        };

        #endregion
        #region # Task Seed(ApplicationDbContext dbContext)

        public static async Task Seed(ApplicationDbContext dbContext)
        {
            try
            {
                dbContext.Balls.AddRange(Balls);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("{SeedError}", ex.Message);
            }
        }

        #endregion
    }
}
