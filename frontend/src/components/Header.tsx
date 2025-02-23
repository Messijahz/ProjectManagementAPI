import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch } from "@fortawesome/free-solid-svg-icons";

interface HeaderProps {
  searchTerm?: string;
  setSearchTerm?: (value: string) => void;
}

const Header = ({ searchTerm = "", setSearchTerm }: HeaderProps) => {
  const navigate = useNavigate();

  return (
    <div className="header-bar">
      <h1 className="title">Projects</h1>
      <div className="header-right">

      {setSearchTerm && (
        <div className="search-container">
          <input
            type="text"
            placeholder="Search"
            className="search-input"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
          <span className="search-icon">
            <FontAwesomeIcon icon={faSearch} />
          </span>
        </div>
      )}
        <button className="new-project-btn" onClick={() => navigate("/create")}>
          New Project
        </button>
      </div>
    </div>
  );
};

export default Header;