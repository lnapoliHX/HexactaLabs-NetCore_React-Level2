import React from "react";
import { FaEdit, FaTrash, FaAngleDoubleLeft } from "react-icons/fa";
import PropTypes from "prop-types";
import { Container, Row, Col, Button } from "reactstrap";

const ProductView = props => {
  return (
    <Container fluid>
      <div className="block-header">
        <h1> Producto</h1>
      </div>
      <div className="info-box">
        <Row>
          <Col lg="2">Nombre</Col>
          <Col>{props.product.name}</Col>
        </Row>
        <Row>
          <Col lg="2">Stock</Col>
          <Col>{props.product.stock}</Col>
        </Row>
        <Row>
          <Col lg="2">Precio Compra</Col>
          <Col>{props.product.costPrice}</Col>
        </Row>
        <Row>
          <Col lg="2">Precio Venta</Col>
          <Col>{props.product.salePrice}</Col>
        </Row>
        <Row>
          <Col lg="2">Categoria</Col>
          <Col>{props.product.productTypeDesc}</Col>
        </Row>
      </div>
      <div className="productType-view__button-row">
        <Button title="Editar" aria-label="Editar"
          className="productType-form__button"
          color="primary"
          onClick={() => props.push(`/product/update/${props.match.params.id}`)}
        ><FaEdit className="productType__button-icon" />Editar
        </Button>
        <Button title="Eliminar" aria-label="Eliminar"
          className="productType-form__button"
          color="danger"
          onClick={() =>
            props.push(`/product/view/${props.match.params.id}/remove`)
          }
        >
          <FaTrash className="productType__button-icon" />Eliminar
        </Button>
        <Button title="Volver" aria-label="Volver"
          className="productType-form__button"
          color="primary"
          onClick={() => props.push(`/product`)}
        ><FaAngleDoubleLeft className="productType__button-icon" />Volver
        </Button>
      </div>
    </Container>
  );
};

ProductView.propTypes = {
  product: PropTypes.object.isRequired,
  push: PropTypes.func.isRequired,
  match: PropTypes.object.isRequired
};

export default ProductView;
