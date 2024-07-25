
<div align="center">
  <br />
  <img src="https://github.com/user-attachments/assets/a46953ad-6b84-425a-8ff0-6bf058b1f78a" alt="Project banner"/>
  <br />

  <div>
    <img src="https://img.shields.io/badge/Next.js-%23000000.svg?style=for-the-badge&logo=nextdotjs&logoColor=white" alt="Next.js" />
    <img src="https://img.shields.io/badge/-Tailwind_CSS-black?style=for-the-badge&logoColor=white&logo=tailwindcss&color=06B6D4" alt="tailwindcss" />
    <img src="https://img.shields.io/badge/-Typescript-black?style=for-the-badge&logoColor=white&logo=typescript&color=3178C6" alt="typescript" />
    <img src="https://img.shields.io/badge/Redis-%23DC382D.svg?style=for-the-badge&logo=redis&logoColor=white" alt="redis" />
    <img src="https://img.shields.io/badge/PostgreSQL-%23336791.svg?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />
    <img src="https://img.shields.io/badge/C%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
    <img src="https://img.shields.io/badge/Serilog-%230D5D90.svg?style=for-the-badge&logo=serilog&logoColor=white" alt="Serilog" />
    <img src="https://img.shields.io/badge/Docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white" alt="Docker" />
  </div>

  <h3 align="center">Social Media App</h3>
  
</div>

## 📋 <a name="table">Table of Contents</a>

1. ⚙️ [Tech Stack](#tech-stack)
2. 🔋 [Features](#features)
3. 🤸 [Quick Start](#quick-start)

## <a name="tech-stack">⚙️ Tech Stack</a>

- NextJS
- Postgresql
- React Query
- TypeScript
- Shadcn
- Aceternity UI
- Tailwind CSS
- C#
- ASP.NET Core
- Mediator
- OpenTelemetry
- Serilog
- Clerk Auth
- Docker
- Redis

## <a name="features">🔋 Features</a>

👉 Authentication System: A robust authentication system with Clerk Auth, ensuring security and user privacy across the Next.js application.

👉 Explore Page: Homepage built with Next.js and TypeScript for users to explore posts, with a featured section for top creators styled using Tailwind CSS and Shadcn components.

👉 Detailed Post Page: A detailed post page displaying content and related posts for an immersive user experience, utilizing React Query for efficient data fetching and caching.

👉 Profile Page: A user profile page showcasing liked posts and providing options to edit the profile, implemented with Aceternity UI components and Tailwind CSS for a seamless user interface.

👉 Browse Other Users: Allow users to browse and explore other users' profiles and posts, leveraging PostgreSQL for efficient data storage and retrieval.

👉 Create Post Page: Implement a user-friendly create post page with effortless file management, storage, and drag-drop feature, built with Next.js and enhanced with TypeScript.

👉 Edit Post Functionality: Provide users with the ability to edit the content of their posts at any time, utilizing ASP.NET Core and C# for backend services, with Mediator pattern to handle commands and queries efficiently.

👉 Responsive UI with Bottom Bar: A responsive UI with a bottom bar, enhancing the mobile app feel for seamless navigation, styled with Tailwind CSS and Shadcn components.

👉 React Query Integration: Incorporate the React Query (Tanstack Query) data fetching library for:

Auto caching to enhance performance,
Parallel queries for efficient data retrieval,
First-class Mutations to handle data updates efficiently.
👉 Logging and Monitoring: Implement comprehensive logging with Serilog and distributed tracing with OpenTelemetry in ASP.NET Core to monitor application performance and ensure reliability.

## <a name="quick-start">🤸 Quick Start</a>

Follow these steps to set up the project locally on your machine.

**Prerequisites**

Make sure you have the following installed on your machine:

- [Git](https://git-scm.com/)
- [Node.js](https://nodejs.org/en)
- [npm](https://www.npmjs.com/) (Node Package Manager)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [Docker](https://www.docker.com/)

**Cloning the Repository**

```bash
git clone https://github.com/HilthonTT/Synapse.git
```

***Frontend***

Navigate to the frontend directory:

```bash
cd src/frontend
```

Install the project dependencies using npm:

```bash
npm install
```

**Set Up Environment Variables**

Create a new file named `.env` in the root of your project and add the following content:

```env
NEXT_PUBLIC_CLERK_PUBLISHABLE_KEY=
CLERK_SECRET_KEY=
NEXT_PUBLIC_CLERK_SIGN_IN_URL=/sign-in
NEXT_PUBLIC_CLERK_SIGN_UP_URL=/sign-up

NEXT_PUBLIC_BASE_URL="http://localhost:3000"

BASE_API_URL="https://localhost:7173/synapse"
API_VERSION="v1"
CLERK_TEMPLATE="synapse"
```

Replace the placeholder values with your actual clerk credentials. You can obtain these by creating an accoun on the [Clerk Website](https://clerk.com).

**Set up a Clerk JWT Template**

Setup a Clerk JWT Template named "synapse" and have those specific fields:

<div align="center">
  ![image](https://github.com/user-attachments/assets/3f44b50a-f024-4495-bb28-e57d4fa068f0)
</div>

**Running the Project**

```bash
npm run dev
```

Open [http://localhost:3000](http://localhost:3000) in your browser to view the project.

***Backend***

Navigate to the backend directory:

```bash
cd src/backend
```

Setup your appsettings.json (normally already prefilled)

```appsetings.json
"ConnectionStrings": {
  "Database": "Host=synapse-database;Port=5432;Database=synapse;Username=postgres;Password=postgres;Include Error Detail=true",
  "BlobStorage": "UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://synapse-blob-storage;",
  "Redis": "synapse.redis:6379"
},
"Serilog": {
  "Using": [
    "Serilog.Sinks.Console",
    "Serilog.Sinks.Seq"
  ],
  "MinimumLevel": {
    "Default": "Information",
    "Override": {
      "Microsoft": "Information"
    }
  },
  "WriteTo": [
    { "Name": "Console" },
    {
      "Name": "Seq",
      "Args": { "serverUrl": "http://synapse.seq:5341" }
    }
  ],
  "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
},
"Blob": {
  "ContainerName": "files"
},
"Jwt": {
  "Authority": "https://big-termite-41.clerk.accounts.dev",
  "AuthorizedParty": "http://localhost:3000"
},
"BackgroundJobs": {
  "Outbox": {
    "Schedule": "0/15 * * * * *"
  }
},
"Sentry": {
  "Dsn": "<YOUR-DNS>",
  "SendDefaultPii": true,
  "MaxRequestBodySize": "Always",
  "MinimumBreadcrumbLevel": "Debug",
  "MinimumEventLevel": "Warning",
  "AttachStackTrace": true,
  "Debug": true,
  "DiagnosticLevel": "Error",
  "TracesSampleRate": 1.0,
  "ProfilesSampleRate": 1.0
}
```

Running the docker yml file:

```bash
docker-compose up
```

This command will start the necessary services as defined in your Docker Compose configuration.

Run the Yarp load balancer:

```bash
dotnet run --project Yarp.LoadBalancer.csproj
```

If everything succeeded, there should be an API at https://localhost:5001/swagger.json and a Yarp Balancer at https://localhost:7173/swagger.json

Enjoy!
