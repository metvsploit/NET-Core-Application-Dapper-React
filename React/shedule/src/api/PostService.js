import axios from "axios"

export default class PostService {

    static async GetAllShedule() {
        try {
            const response = await axios.get ("https://localhost:5001/api/shedule");
            const data = await response.data.data;
            return data;
        }
        catch(e) {
            return e.response;
        }
    };

    static async GetSheduleByName(name) {
        try {
            const response = await axios.get("https://localhost:5001/api/shedule/" + name);
            const data = await response.data.data;
            return data;
        }
        catch(e) {
            console.log(e);
        }
    };

    static async Login(model) {
        try {
            const response = await axios.post("https://localhost:5001/api/user/login", model);
            
            return response;
        }
        catch(e) {
            return(e.response.data);
        }
    }

    static async Logout() {
        try {
            const response = await axios.get("https://localhost:5001/api/user/logout");
            return response;
        }
        catch(e) {
            return(e.response);
        }
    }

    static async GetUserData() {
        try {
            const headers = {
                "Authorization": "Bearer " + localStorage.getItem("Bearer")
            }
            
            
            if(localStorage.getItem("Role") === "Student") {
               const response = await axios.get("https://localhost:5001/api/student/user/",{headers});
               return response;
            }
            if(localStorage.getItem("Role") === "Teacher") {
                const response = await axios.get("https://localhost:5001/api/teacher/user/",{headers});
                return response;
             }
        }
        catch(e) {
            return(e.response);
        }
    }

    static async CreateShedule(model) {
        try {
            const shedule = {
                directionId: model.directionId,
                teacherId: model.teacherId,
                date: model.date.toJSON()
            }
            console.log(shedule)
            const headers = {
                "Authorization": "Bearer " + localStorage.getItem("Bearer")
            }
            const response = await axios.post("https://localhost:5001/api/shedule", shedule, {headers});
            return response;
        }
        catch(e) {
            return(e.response);
        }
    }

    static async GetSheduleByTeacher(id) {
        try{
            const response = await axios.get("https://localhost:5001/api/shedule/teacher/" + id);
            return response.data.data;
        }
        catch(e) {
            return(e.response);
        }
    }

    static async DeleteSheduleById(id) {
        try {
            const headers = {
                "Authorization": "Bearer " + localStorage.getItem("Bearer")
            }
            const response = await axios.delete("https://localhost:5001/api/shedule/" + id, {headers});
            return response.data.message;
        }
        catch(e) {
            return(e.response);
        }
    }
}