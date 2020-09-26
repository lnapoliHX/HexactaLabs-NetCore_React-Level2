import React from "react";
import PropTypes from "prop-types";
import { Container, Row, Col, Button } from "reactstrap";

const ProductView = props => {
  return (
    <Container fluid>
      <h1>{props.product.name}</h1>
      <Row>
        <Col lg="2">Stock</Col>
        <Col>{props.product.stock}</Col>
      </Row>
      <div className="store-view__button-row">
        <Button
          className="store-form__button"
          color="primary"
          onClick={() => props.push(`/product/update/${props.match.params.id}`)}
        >
          Editar
        </Button>
        <Button
          className="store-form__button"
          color="danger"
          onClick={() =>
            props.push(`/product/view/${props.match.params.id}/remove`)
          }
        >
          Eliminar
        </Button>
        <Button
          className="store-form__button"
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
