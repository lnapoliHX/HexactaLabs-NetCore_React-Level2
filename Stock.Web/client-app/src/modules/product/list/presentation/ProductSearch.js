import React from "react";
import PropTypes from "prop-types";
import { Row, Col, Input, Button } from "reactstrap";
import { MdSearch, MdCancel } from "react-icons/md";

const Search = props => {
  return (
    <React.Fragment>
      <h4>Búsqueda</h4>
      <Row>
        <Col>
		<Input
					name="name"
					id="nameInput"
					type="text"
					onChange={props.handleFilter}
					value={props.filters.name}
					placeholder="Nombre"
				/>
        </Col>
        <Col>
		<Input
					name="costprice"
					id="costpriceInput"
					type="text"
					onChange={props.handleFilter}
					value={props.filters.costprice}
					placeholder="Costo"
				/>
        </Col>
         <Col>
		<Input
					name="saleprice"
					id="salepriceInput"
					type="text"
					onChange={props.handleFilter}
					value={props.filters.saleprice}
					placeholder="Precio de venta"
				/>
        </Col>
		 <Col>
		<Input
					name="producttypedesc"
					id="producttypedescInput"
					type="text"
					onChange={props.handleFilter}
					value={props.filters.producttypedesc}
					placeholder="Categoría"
				/>
        </Col>
        <Col>
          <Button color="primary" onClick={props.submitFilter}>
            <MdSearch /> Buscar
          </Button>
          <Button color="primary" className="ml-3" onClick={props.clearFilter}>
            <MdCancel /> Limpiar
          </Button>
        </Col>
      </Row>
    </React.Fragment>
  );
};

Search.propTypes = {
  handleFilter: PropTypes.func.isRequired,
  submitFilter: PropTypes.func.isRequired,
  clearFilter: PropTypes.func.isRequired,
  filters: PropTypes.object.isRequired
};

export default Search;
