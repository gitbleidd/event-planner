import './App.css';
import NavBar from "./components/NavBar";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from './pages/Home';
import Event from './pages/Event';
import Events from './pages/Events';
import EventEditCreation from './pages/EventEditCreation';
import * as routes from "./shared/routes"

function App() {
  return (
    <BrowserRouter>
      <NavBar />
      <Routes>
        <Route path={routes.homeRoute} element={<Home/>}/>
        <Route path={routes.newEventRoute} element={<EventEditCreation/>}/>
        <Route path={routes.eventsRoute} element={<Events/>}/>
        <Route path={routes.eventIdRoute} element={<Event/>}/>
        <Route path={routes.editEventRoute} element={<EventEditCreation/>}/>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
