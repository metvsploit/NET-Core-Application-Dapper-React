import 'bootstrap/dist/css/bootstrap.min.css';
import Navibar from './components/Navibar';
import {useEffect, useState} from "react";
import { BrowserRouter,Route } from "react-router-dom";
import AppRouter from './components/AppRouter';
import { AuthContext } from "./context";


function App() {
  const [isAuth, setAuth] = useState(false);
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    if (localStorage.getItem('Bearer')) {
        setAuth(true)
    }
    setLoading(false);
}, [])


  return (
    document.body.style = 'background: #1c1a1a;',
    <div className="App">
      <AuthContext.Provider value={{
      isAuth,
      setAuth,
      isLoading
      }}>
       <BrowserRouter>
         <Navibar/>
         <AppRouter/>
       </BrowserRouter>
      </AuthContext.Provider>    
    </div>
  ) 
}

export default App;
