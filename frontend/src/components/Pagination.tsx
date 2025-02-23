interface PaginationProps {
    currentPage: number;
    totalPages: number;
    projectsPerPage: number;
    onPageChange: (page: number) => void;
    onItemsPerPageChange: (items: number) => void;
  }
  
  const Pagination = ({ currentPage, totalPages, projectsPerPage, onPageChange, onItemsPerPageChange }: PaginationProps) => {
    return (
      <div className="pagination-footer">


        <div className="items-per-page">
          <label>Items per page:</label>
          <select value={projectsPerPage} onChange={(e) => onItemsPerPageChange(Number(e.target.value))}>
            <option value={5}>5</option>
            <option value={10}>10</option>
            <option value={15}>15</option>
            <option value={20}>20</option>
            <option value={25}>25</option>
          </select>
        </div>
  

        <div className="pagination-controls">
          <button onClick={() => onPageChange(1)} disabled={currentPage === 1}>⏮</button>
          <button onClick={() => onPageChange(currentPage - 1)} disabled={currentPage === 1}>◀</button>
          <span> {currentPage} of {totalPages} </span>
          <button onClick={() => onPageChange(currentPage + 1)} disabled={currentPage === totalPages}>▶</button>
          <button onClick={() => onPageChange(totalPages)} disabled={currentPage === totalPages}>⏭</button>
        </div>
      </div>
    );
  };
  
  export default Pagination;
  