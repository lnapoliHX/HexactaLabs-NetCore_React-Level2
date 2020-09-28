import React from "react";
import { Link } from "react-router-dom";
import {
  FaEdit,
  FaTrash,
  FaSearch,
  FaRegArrowAltCircleUp,
  FaRegArrowAltCircleDown,
} from "react-icons/fa";

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

  let incStockButton = (
    <Link className="product-list__button" to={`/product/incStock/${value}`}>
      <FaRegArrowAltCircleUp className="product-list__button-icon" />
    </Link>
  );

  let decStockButton = (
    <Link className="product-list__button" to={`/product/decStock/${value}`}>
      <FaRegArrowAltCircleDown className="product-list__button-icon" />
    </Link>
  );

  return (
    <span>
      {viewButton} {editButton} {removeButton} {incStockButton} {decStockButton}
    </span>
  );
};

const HeaderComponent = (props) => {
  return <h2 className="tableHeading">{props.title}</h2>;
};

HeaderComponent.displayName = "HeaderComponent";

const columns = [
  {
    Header: <HeaderComponent title="Nombre" />,
    accessor: "name",
    Cell: (props) => props.value,
  },
  {
    Header: <HeaderComponent title="Stock" />,
    accessor: "stock",
    Cell: (props) => props.value,
  },
  {
    Header: <HeaderComponent title="Categoria" />,
    accessor: "productTypeDesc",
    Cell: (props) => props.value,
  },
  {
    Header: <HeaderComponent title="Proveedor" />,
    accessor: "providerName",
    Cell: (props) => props.value,
  },
  {
    Header: <HeaderComponent title="Acciones" />,
    accessor: "id",
    Cell: renderToolbar,
  },
];

renderToolbar.propTypes = {
  value: PropTypes.string.isRequired,
};

HeaderComponent.propTypes = {
  title: PropTypes.string.isRequired,
};

export default columns;
