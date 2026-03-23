# 🚗 Dream Garage

> *Built as a school assignment to connect a HTML/CSS/JS frontend with a C# API.*

---

## About

This program is a simple form storing information about an entity with three attributes (four with ID). Theme: a car garage. Fill out the make, model and year — it gets stored on your local SQL database.

---

## Getting Started

```bash
git clone <repourl>
cd FullstackAssignment2
dotnet ef database update
```
Then launch in Visual Studio or your preferred code editor.
```
Quick troubleshooting:
Database connection errors: Change Connectionstring to custom string in appsettings.json.
Localhost connection errors: Chosen localhost port can be occupied on your system, choose a different one in program.cs

---

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/api/car` | Get all cars |
| `GET` | `/api/car/{id}` | Get one car |
| `POST` | `/api/car` | Add a car |
| `PUT` | `/api/car/{id}` | Update a car |
| `DELETE` | `/api/car/{id}` | Delete a car |

---

## How the Frontend and Backend Talk

The FE and BE communicate through the API endpoints over HTTPS. The controller receives the request and passes it to the DTO, which uses FluentValidation to check the values. The request then goes to the service, which talks to the database through EF Core. C# returns a response in JSON and `app.js` receives it and renders the frontend list.

---

## Reflection

The C# part was straightforward — setting up an API is something we have done many times. The struggle was understanding JS and diagnosing it. The most common faults were typos. This isn't a very "smart" or innvative program but I think it is well working and handles faults and issues pretty well. In the future I will focus more on creatimng apps with a real purpose, this was strictly for understanding how I connect my FE and BE.

It was really fun building a fullstack program and understanding how these things connect. 🙂

