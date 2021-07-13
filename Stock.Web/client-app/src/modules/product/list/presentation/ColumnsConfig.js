import React from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import { FaEdit, FaTrash, FaSearch } from "react-icons/fa";

const renderToolbar = ({ value }) => {
  let viewButton = (
    <Link className="productType-list__button" to={`/product/view/${value}`}>
      <FaSearch className="productType-list__button-icon" />
    </Link>
  );

  let editButton = (
    <Link className="productType-list__button" to={`/product/update/${value}`}>
      <FaEdit className="productType-list__button-icon" />
    </Link>
  );

  let removeButton = (
    <Link className="productType-list__button" to={`/product/remove/${value}`}>
      <FaTrash className="productType-list__button-icon" />
    </Link>
  );

  return (
    <span>
      {viewButton} {editButton} {removeButton}
    </span>
  );
};

const HeaderComponent = props => {
  return (
    <div
      style={{
        textAlign: "left",
        fontWeight: "bold"
      }}
    >
      {props.title}
    </div>
  );
};

HeaderComponent.displayName = "HeaderComponent";

const columns = [
  {
    Header: <HeaderComponent title="Nombre Producto" />,
    accessor: "name",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Stock" />,
    accessor: "stock",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Costo de Compra" />,
    accessor: "costPrice",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Costo de Venta" />,
    accessor: "salePrice",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Categoria" />,
    accessor: "productTypeDesc",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Acciones" />,
    accessor: "id",
    Cell: renderToolbar
  }
];

renderToolbar.propTypes = {
  value: PropTypes.string.isRequired
};

HeaderComponent.propTypes = {
  title: PropTypes.string.isRequired
};

export default columns;
