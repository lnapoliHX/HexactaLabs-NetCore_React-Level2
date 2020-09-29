import React from "react";
import { Link } from "react-router-dom";
import { FaEdit, FaTrash, FaSearch, FaCartPlus, FaCartArrowDown } from "react-icons/fa";

import PropTypes from "prop-types";

const renderToolbar = ({ value }) => {
  let viewButton = (
    <Link className="product-list__button" to={`/product/view/${value}`}>
      <FaSearch className="product-list__button-icon" title="Ver producto"/>
    </Link>
  );

  let editButton = (
    <Link className="product-list__button" to={`/product/update/${value}`}>
      <FaEdit className="product-list__button-icon" title="Editar producto"/>
    </Link>
  );

  let removeButton = (
    <Link className="product-list__button" to={`/product/remove/${value}`}>
      <FaTrash className="product-list__button-icon" title="Eliminar producto"/>
    </Link>
  );

  let incrementButton = (
    <Link className="product-list__button" to={`/product/stock/increase/${value}`} title="Incrementar stock">
      <FaCartPlus className="product-list__button-icon" />
    </Link>
  );

  let decrementButton = (
    <Link className="product-list__button" to={`/product/stock/decrease/${value}`} title="Decrementar stock">
      <FaCartArrowDown className="product-list__button-icon" />
    </Link>
  );

  return (
    <span>
      {viewButton} {editButton} {removeButton} {incrementButton} {decrementButton}
    </span>
  );
};

const HeaderComponent = props => {
  return (
	<h2 
	className="tableHeading"
    >
      {props.title}
    </h2>
  );
};

HeaderComponent.displayName = "HeaderComponent";

const columns = [
  {
    Header: <HeaderComponent title="Nombre" />,
    accessor: "name",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Costo" />,
    accessor: "costPrice",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Precio" />,
    accessor: "salePrice",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Categoría" />,
    accessor: "productTypeDesc",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Stock" />,
    accessor: "stock",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Acciones"/>,
    accessor: "id",
    Cell: renderToolbar,
    minWidth: 140
  }
];

renderToolbar.propTypes = {
  value: PropTypes.string.isRequired
};

HeaderComponent.propTypes = {
  title: PropTypes.string.isRequired
};

export default columns;