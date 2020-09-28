import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { getProductById } from "../../list";
import { decreaseStock } from "..";
import Form from "../presentation";
import { Container, Row, Col } from "reactstrap";
import { goBack } from "connected-react-router";

const Stock = ({
  initialValues,
  decreaseStock: onSubmit,
  goBack: onCancel,
}) => {
  return (
    <Container fluid>
      <Row>
        <Col>
          <div className="block-header">
            <h1>Manejo de stock</h1>
          </div>
        </Col>
      </Row>
      <Row>
        <Col>
          <Form
            initialValues={initialValues}
            onSubmit={onSubmit}
            handleDecrease={onCancel}
          />
        </Col>
      </Row>
    </Container>
  );
};

Stock.propTypes = {
  initialValues: PropTypes.object.isRequired,
  decreaseStock: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
};

const mapStateToProps = (state, ownProps) => ({
  initialValues: getProductById(state, ownProps.match.params.id),
});

const mapDispatchToProps = {
  decreaseStock,
  goBack,
};

export default connect(mapStateToProps, mapDispatchToProps)(Stock);
