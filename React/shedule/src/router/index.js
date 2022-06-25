import Profile from "../pages/Profile";
import Shedule from "../pages/Shedule";
import SheduleDirection from "../pages/SheduleDirection";

export const privateRoutes = [
    {path: '/account/profile', element: <Profile/>, exact: true},
    {path: '/shedule', element: <Shedule/>, exact: true},
    {path: '/shedule/:name', element: <SheduleDirection/>, exact: true},
]

export const publicRoutes = [
    {path: '/shedule', element: <Shedule/>, exact: true},
    {path: '/shedule/:name', element: <SheduleDirection/>, exact: true},
]