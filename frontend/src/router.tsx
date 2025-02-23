import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import CreateProject from "./pages/CreateProject";
import EditProject from "./pages/EditProject";

function AppRouter() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/create" element={<CreateProject />} />
        <Route path="/edit/:id" element={<EditProject />} />
      </Routes>
    </Router>
  );
}

export default AppRouter;