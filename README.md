# Steps

## Identified key features:

- Survey creation, scheduling, rescheduling, and closing
- Survey question management
- Survey response collection
- Survey listing, pagination, and search\*
- Survey ownership and user association (no auth)

## Defined business rules:

- Surveys can only be scheduled for future dates.
- Survey end date must be at least one hour after the start.
- Status transitions must be sequential and irreversible.\*

# Project Setup

- created ASP.NET Core MVC project
- Entity Framework Core as ORM
- Crate Infrastucture and Domain projects
- Add duture Entities to Domain
- Add dbcontext to Infrastucture
- Add business rules to Survey Entity itself
- Add service to communicate w dbcontext
- Add Surveys Controller w update, create, delete functionality
- Add to Surveys Controller list and other view endpoints
- Crate blazor views(test different approaches)
- test functionality

# Future Steps

- Make status transitions sequential and irreversible
- Move db context usage to deeper level(Infrastucture) (IRepository)
- Implement search for Surveys
- Add auth
- Add constraints for start/end date in dbcontext
- Implement answer functionality
