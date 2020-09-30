import React from "react";
import { Link } from "react-router-dom";
import { FaEdit, FaTrash, FaSearch, FaPlus, FaMinus } from "react-icons/fa";
import PropTypes from "prop-types";

const renderToolbar = ({ value }) => {
  let viewButton = (
    <Link className="product-list__button" to={`/product/view/${value}`}>
      <FaSearch className="product-list__button-icon" />
    </Link>
  );

  let editButton = (
    <Link className="product-list__button" to={`/product/update/${value}`}>
      <FaEdit className="product-list__button-icon" />
    </Link>
  );

  let removeButton = (
    <Link className="product-list__button" to={`/product/remove/${value}`}>
      <FaTrash className="product-list__button-icon" />
    </Link>
  );

  let increaseStock = (
    <Link className="product-list__button" to={`/product/addStock/${value}`}>
      <FaPlus className="product-list__button-icon" />
    </Link>
  );

  let decreaseStock = (
    <Link className="product-list__button" to={`/product/subtractStock/${value}`}>
      <FaMinus className="product-list__button-icon" />
    </Link>
  );

  return (
    <span>
      {increaseStock}{decreaseStock} {viewButton} {editButton} {removeButton}
    </span>
  );
};


const HeaderComponent = props => {
  return (
	<h2 className="tableHeading">
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
    Header: <HeaderComponent title="Precio de costo" />,
    accessor: "costPrice",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Precio de venta" />,
    accessor: "salePrice",
    Cell: props => props.value
  },
  {
    Header: <HeaderComponent title="Stock" />,
    accessor: "stock",
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