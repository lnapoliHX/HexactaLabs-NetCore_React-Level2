import React from "react";
import PropTypes from "prop-types";
import { Container, Row, Col, Button } from "reactstrap";

const ProductView = props => {
  return (
    <Container fluid>
      <h1>{props.product.name}</h1>
      {/*
      <Row>
        <Col lg="2">Categoría</Col>
        <Col>{props.product.productTypeName}</Col>
      </Row>
      <Row>
        <Col lg="2">Proveedores</Col>
        <Col>{props.product.providers}</Col>
      </Row>
      */}
      <Row>
        <Col lg="2">Stock</Col>
        <Col>{props.product.stock}</Col>
      </Row>
      <Row>
        <Col lg="2">Costo</Col>
        <Col>{props.product.costPrice}</Col>
      </Row>
      <Row>
        <Col lg="2">Precio de Venta</Col>
        <Col>{props.product.salePrice}</Col>
      </Row>
      <Row>
        <Col lg="2">Categoría</Col>
        <Col>{props.product.productTypeDesc}</Col>
      </Row>
      <Row>
        <Col lg="2">Proveedor</Col>
        <Col>{props.product.providerName}</Col>
      </Row>
      <div className="product-view__button-row">
        <Button
          className="product-form__button"
          color="primary"
          onClick={() => props.push(`/product/update/${props.match.params.id}`)}
        >
          Editar
        </Button>
        <Button
          className="product-form__button"
          color="danger"
          onClick={() =>
            props.push(`/product/view/${props.match.params.id}/remove`)
          }
        >
          Eliminar
        </Button>
        <Button
          className="product-form__button"
          color="default"
          onClick={() => props.push(`/product`)}
        >
          Volver
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
