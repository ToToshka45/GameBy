import UserAuth from "../../../interfaces/UserAuth";

export const users: UserAuth[] = [
        {   
            id: 1,
            username: "admin",
            email: "admin@admin.ru",
            password: "admin",
            accessToken: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxIiwibmFtZSI6ImFkbWluIiwiaWF0IjoxNzQwNzc1NTU0LCJleHAiOjE3NDA3NzkxNTQsInJvbGVzIjpbImFkbWluIl19.mXqzbBRb-pGHsZAqg-B1qaug3Bny-aq0eD2AA21EIiw",
            refreshToken: "refreshToken1",
            roles: [] 
        },
        {   
            id: 2,
            username: "org",
            email: "admin@admin.ru",
            password: "org",
            accessToken: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIzIiwibmFtZSI6Im9yZ2FuaXplciIsImlhdCI6MTc0MDc4Mzg4NCwiZXhwIjoxNzQwNzg3NDg0LCJyb2xlcyI6WyJvcmdhbml6ZXIiXX0.M2NXu1sdiRwIt46iBUFJxuq8Ky7c3RBfK8CTMcHOmKE",
            refreshToken: "refreshToken2",
            roles: [] 
        },
        {   
            id: 3,
            username: "gamer",
            email: "gamer@gamer.ru",
            password: "gamer",
            accessToken: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIyIiwibmFtZSI6Ik5pY2VHdXkiLCJpYXQiOjE3NDA3ODM3OTMsImV4cCI6MTc0MDc4NzM5Mywicm9sZXMiOlsiZ2FtZXIiXX0.Pre0tE0TUZgHYpGw-1h_bDEbxYWlqOqBH57pt_iLeA4",
            refreshToken: "refreshToken3",
            roles: [] 
        },

    ]